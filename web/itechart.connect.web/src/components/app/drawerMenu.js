import React from 'react';
import Drawer from 'material-ui/Drawer';
import AppBar from 'material-ui/AppBar';

import NavigationPanel from './navigationPanel.js';

export default class DrawerMenu extends React.Component {
    render() {
        return (
            <Drawer className='drawer-menu-container' width={150} openSecondary={false} open={true} showMenuIconButton={false}>
                <div className='drawer-menu-content'>
                    <AppBar/>
                    <NavigationPanel/>
                </div>
            </Drawer>
        );
    }
}
