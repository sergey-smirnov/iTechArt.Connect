import { connect } from 'react-redux';

import DrawerMenu from '../components/app/drawerMenu.js';

const mapStateToProps = (state) => {
    return {}
}

const mapDispatchToProps = (dispatch) => {
    return {}
}

const NavigationMenu = connect(
    mapStateToProps,
    mapDispatchToProps
)(DrawerMenu)

export default NavigationMenu;
