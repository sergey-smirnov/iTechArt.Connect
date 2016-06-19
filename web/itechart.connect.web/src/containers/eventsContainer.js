import { connect } from 'react-redux';

import Events from '../components/events/events.js';

import { Events as eventsData } from '../data/events.js';

const mapStateToProps = (state) => {
    return {
        events: eventsData
    }
}

const mapDispatchToProps = (dispatch) => {
    return {}
}

const EventsContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(Events)


export default EventsContainer;
