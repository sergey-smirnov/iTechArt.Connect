'use strict';

import lodash from 'lodash';
import React from 'react';
import Immutable from 'immutable';
import { Actions as UserActions } from '../constants/user';

const InitialState = Immutable.Map({
    token: null,
    authenticated: false,
    profile: null,
    isAuthenticationInProgress: false,
    image: null
});

export function user(state = InitialState, action) {
    switch (action.type) {
        case UserActions.UPDATE_IMAGE:
            return state
                .set('image', action.imageUrl)
                .set('isAuthenticationInProgress', false);
        case UserActions.REQUEST_AUTHENTICATION:
            return state
                .delete('profile')
                .set('authenticated', false)
                .set('isAuthenticationInProgress', true);
        case UserActions.AUTHENTICATION_FAILED:
            return state
                .delete('profile')
                .set('authenticated', false)
                .set('isAuthenticationInProgress', false);
        case UserActions.AUTHENTICATED:
            return state
                .set('token', action.token)
                .delete('profile')
                .set('authenticated', true)
                .set('isAuthenticationInProgress', false);
        case UserActions.UNAUTHENTICATE:
            return state
                .delete('profile')
                .set('authenticated', false);
        default:
            return state;
    }
}
