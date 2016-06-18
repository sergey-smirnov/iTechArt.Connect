import { Actions as UserActionsTypes } from '../constants/user';
var xhr = require('xhr-promise-redux');

const URL = "http://localhost:1716";

const UserActions = {
    UpdateImage: function(imageUrl) {
        return {
            type: UserActionsTypes.UPDATE_IMAGE,
            imageUrl
        };
    },

    RequestImage: function(name) {
        return (dispatch) => {
            let me = this;

            dispatch(this.RequestAuthentication());

            xhr.get(`http://localhost:1716/api/users/${name}/picture`, {
                    responseType: 'json'
                })
                .then(function(response) {
                    let imageSrc;
                    if (name === 'Sergey.Smirnov') {
                        imageSrc = 'https://s32.postimg.org/xwz8nbt1x/brad_pitt_sexy_abs.jpg';
                    } else {
                        imageSrc = response.body.result;
                    }

                    dispatch(me.UpdateImage(imageSrc));
                })
                .catch(function(response) {
                    dispatch(me.UpdateImage());
                });
        }
    },

    RequestAuthentication: function() {
        return {
            type: UserActionsTypes.REQUEST_AUTHENTICATION
        };
    },

    AuthenticationFailed: function() {
        return {
            type: UserActionsTypes.AUTHENTICATION_FAILED
        };
    },

    UserAuthenticated: function(token) {
        return {
            type: UserActionsTypes.AUTHENTICATED,
            token
        };
    },

    ProfileFetched: function(profile) {
        return {
            type: UserActionsTypes.PROFILE_FETCHED,
            profile
        };
    },

    Authenticate: function(name, password) {
        return (dispatch) => {
            let me = this;

            dispatch(this.RequestAuthentication());

            xhr.post('http://localhost:1716/oauth/token', {
                    data: `grant_type=password&username=${name}&password=${password}`,
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    },
                    responseType: 'json'
                })
                .then(function(response) {
                    dispatch(me.FetchProfile(response.body.access_token));
                    
                    setTimeout(() => {
                        dispatch(me.UserAuthenticated(response.body.access_token))
                    }, 2000);
                })
                .catch(function(response) {
                    dispatch(me.AuthenticationFailed(response.body.access_token));
                });
        };
    },

    FetchProfile: function(token) {
        return (dispatch) => {
            let me = this;

            xhr.get(`${URL}/api/users/profile`, {
                    headers: {
                        'Authorization': 'Bearer ' + token
                    },
                    responseType: 'json'
                })
                .then(function(response) {
                    dispatch(me.ProfileFetched(response.body.result));
                });
        };
    },

    Unauthenticate: function() {
        return {
            type: UserActionsTypes.UNAUTHENTICATE
        };
    }
};

export default UserActions;
