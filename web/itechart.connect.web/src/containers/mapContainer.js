import { connect } from 'react-redux';

import MapComponent from '../components/map/mapComponent.js';

import { Events } from '../data/events.js';

const mapStateToProps = (state) => {
    return {
        showEventsList: true
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        events: Events
    }
}

const MapContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(MapComponent)


export default MapContainer;
