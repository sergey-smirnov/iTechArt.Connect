'use strict';

import lodash from 'lodash';
import React from 'react';
import Immutable from 'immutable';
import { Actions as UserActions } from '../constants/user';

import fetch from 'isomorphic-fetch';

const InitialState = Immutable.Map({
    authenticated: false,
    profile: null,
    isAuthenticationInProgress: false
});

export function user(state = InitialState, action) {
    switch (action.type) {
        case UserActions.REQUEST_AUTHENTICATION:
            return state
                .delete('profile')
                .set('authenticated', false)
                .set('isAuthenticationInProgress', true);
        case UserActions.AUTHENTICATED:
            return state
                .set('profile', action.profile)
                .set('authenticated', true)
                .set('isAuthenticationInProgress', false);
            // case UserActions.AUTHENTICATE:
            //     return state
            //         .set('profile', { name: action.name })
            //         .set('authenticated', true);
        case UserActions.UNAUTHENTICATE:
            return state
                .delete('profile')
                .set('authenticated', false);
        default:
            return state;
    }
}
