import { connect } from 'react-redux';

import LoginForm from '../components/user/loginForm.js';
import UserActions from '../actions/userActions.js';

const mapStateToProps = (state) => {
    return {
        authenticated: state.UserReducer.get("authenticated"),
        isInProgress: state.UserReducer.get("isAuthenticationInProgress")
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onSuccess: (username) => { dispatch(UserActions.Authenticate(username)) }
    }
}

const UserLogin = connect(
    mapStateToProps,
    mapDispatchToProps
)(LoginForm)


export default UserLogin;
