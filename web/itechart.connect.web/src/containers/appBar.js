import { connect } from 'react-redux';

import TopBar from '../components/app/topBar.js';

const mapStateToProps = (state) => {
    return {
        authenticated: state.UserReducer.get("authenticated")
    }
}

const mapDispatchToProps = (dispatch) => {
    return {}
}

const AppBar = connect(
    mapStateToProps,
    mapDispatchToProps
)(TopBar)

export default AppBar;
