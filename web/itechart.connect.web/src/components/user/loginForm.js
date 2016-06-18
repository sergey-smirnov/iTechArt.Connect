import React, { PropTypes, Component } from 'react'

import TextField from 'material-ui/TextField';
import RaisedButton from 'material-ui/RaisedButton';
import Paper from 'material-ui/Paper';

import RefreshIndicator from 'material-ui/RefreshIndicator';
import CircularProgress from 'material-ui/CircularProgress';

const style = {
    container: {
        position: 'relative'
    },
    refresh: {
        display: 'inline-block',
        position: 'relative',
    },
};

const userImageStyle = {
    height: 150,
    width: 150,
    display: 'block',
    margin: '0 auto',
    textAlign: 'center',
    overflow: 'hidden'
};

const imageStyle = {
    maxWidth: '100%',
    maxHeight: '100%',
    minWidth: '100%',
    minHeight: '100%'
};


class LoginForm extends Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
        };
    }

    onLoginButtonClick() {
        this.props.onLogin(this.refs.name.input.value, this.refs.password.input.value);
    }

    onNameChanged() {
        let name = this.refs.name.input.value;

        this.props.onRequestImage(name);
    }

    render() {
        return (
          <div className='login-form'>
            <Paper>
                <Paper style={userImageStyle} zDepth={1} circle={true}>
                  {this.props.isInProgress ? <CircularProgress size={2.1} /> : <img style={imageStyle} src={this.props.userImage} />}
                </Paper>
                <br />
                <TextField
                   ref='name'
                   hintText='Enter your domain name'
                   fullWidth={true}
                   floatingLabelText='Name'
                   onChange={this.onNameChanged.bind(this)}
                />
                <br />
                <TextField
                    ref='password'
                    hintText='Password'
                    fullWidth={true}
                    floatingLabelText='Password'
                    type='password'
                />
                <br />
                <div className='form-buttons'>
                    <RaisedButton
                        onClick={this.onLoginButtonClick.bind(this)}
                        className='login-button'
                        label='Login'
                        labelPosition='before'
                        primary={true}
                    />
                </div>
              </Paper>
            </div>
        );
    }
}

LoginForm.propTypes = {
    authenticated: PropTypes.bool.isRequired,
    isInProgress: PropTypes.bool.isRequired,
    userImage: PropTypes.string,
    onLogin: PropTypes.func.isRequired,
    onRequestImage: PropTypes.func.isRequired
}

export default LoginForm
