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
                    dispatch(me.UpdateImage(response.body.result));
                })
                .catch(function(response) {
                    // dispatch(me.AuthenticationFailed(response.body.access_token));
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
                    setTimeout(() => {
                        dispatch(me.UserAuthenticated(response.body.access_token))
                    }, 2000);
                })
                .catch(function(response) {
                    dispatch(me.AuthenticationFailed(response.body.access_token));
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
