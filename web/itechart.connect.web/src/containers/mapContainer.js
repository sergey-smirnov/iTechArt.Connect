import { connect } from 'react-redux';

import MapComponent from '../components/map/mapComponent.js';

const mapStateToProps = (state) => {
    return {}
}

const mapDispatchToProps = (dispatch) => {
    return {}
}

const MapContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(MapComponent)


export default MapContainer;
