import { connect } from 'react-redux';

import LoginForm from '../components/user/loginForm.js';
import UserActions from '../actions/userActions.js';

import lodash from 'lodash';

const mapStateToProps = (state) => {
    return {
        authenticated: state.UserReducer.get("authenticated"),
        isInProgress: state.UserReducer.get("isAuthenticationInProgress"),
        userImage: state.UserReducer.get('image')
    }
}

const mapDispatchToProps = (dispatch) => {
    let lazyDispatch = lodash.debounce((username) => { dispatch(UserActions.RequestImage(username)) }, 500);

    return {
        onRequestImage: lazyDispatch,
        onLogin: (username, password) => { dispatch(UserActions.Authenticate(username, password)) }
    }
}

const UserLogin = connect(
    mapStateToProps,
    mapDispatchToProps
)(LoginForm)

export default UserLogin;
