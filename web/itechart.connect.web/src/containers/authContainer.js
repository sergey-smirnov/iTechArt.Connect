import { connect } from 'react-redux';

import AuthComponent from '../components/app/authComponent.js';
import UserLogin from './userLogin.js';

const mapStateToProps = (state, ownProps) => {
    return {
        authenticated: state.user.get("authenticated"),
        defaultComponent: UserLogin,
        children: ownProps.children
    }
}

const mapDispatchToProps = (dispatch) => {
    return {}
}

const AuthContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(AuthComponent)

export default AuthContainer;
