import { Actions as UserActionsTypes } from '../constants/user';

const UserActions = {
    RequestAuthentication: function() {
        return {
            type: UserActionsTypes.REQUEST_AUTHENTICATION
        };
    },

    UserAuthenticated: function(profile) {
        return {
            type: UserActionsTypes.AUTHENTICATED,
            profile
        };
    },

    Authenticate: function(name, password) {
        return (dispatch) => {
            dispatch(this.RequestAuthentication());

            setTimeout(() => { dispatch(this.UserAuthenticated({ name: 'sergey' })) }, 3000);
        };
    },

    Unauthenticate: function() {
        return {
            type: UserActionsTypes.UNAUTHENTICATE
        };
    }
};

export default UserActions;
