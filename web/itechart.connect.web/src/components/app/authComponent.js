import React, { PropTypes } from 'react';

const AuthComponent = React.createClass({
    propTypes: {
        authenticated: PropTypes.bool.isRequired,
        children: PropTypes.any.isRequired,
        defaultComponent: PropTypes.any.isRequired
    },
    render: function() {
        return this.props.authenticated ? this.props.children : React.createElement(this.props.defaultComponent);
    }
});

export default AuthComponent;
