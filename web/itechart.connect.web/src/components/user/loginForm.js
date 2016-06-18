import React, { PropTypes } from 'react'

import RaisedButton from 'material-ui/RaisedButton';

import RefreshIndicator from 'material-ui/RefreshIndicator';

const style = {
    container: {
        position: 'relative',
    },
    refresh: {
        display: 'inline-block',
        position: 'relative',
    },
};

const LoginForm = ({ authenticated, isInProgress, onSuccess }) => (
  <div>
    <RaisedButton label="Primary" primary={true} onClick={onSuccess} />
    <RefreshIndicator
          size={40}
          left={10}
          top={100}
          status="loading"
          style={{display: isInProgress? 'inline-block' : 'none'}}
        />
  </div>
);

LoginForm.propTypes = {
    authenticated: PropTypes.bool.isRequired,
    isInProgress: PropTypes.bool.isRequired,
    onSuccess: PropTypes.func.isRequired
}

export default LoginForm
