import { connect } from 'react-redux';

import TopBar from '../components/app/topBar.js';
import UserActions from '../actions/userActions.js';

const mapStateToProps = (state) => {
    return {
        authenticated: state.user.get("authenticated")
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLogout: () => { dispatch(UserActions.Unauthenticate()) }
    }
}

const AppBar = connect(
    mapStateToProps,
    mapDispatchToProps
)(TopBar)

export default AppBar;
