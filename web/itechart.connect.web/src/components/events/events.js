import React, { PropTypes } from 'react';
import EventsList from './eventsList.js';

const Events = React.createClass({
    propTypes: {
        events: PropTypes.array
    },
    render: function() {
        return (
          <EventsList events={this.props.events} />
        )
    }
});

export default Events;
