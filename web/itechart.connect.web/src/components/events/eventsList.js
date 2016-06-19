import React, { PropTypes } from 'react';
import EventCard from './eventCard.js';

var jquery = require('jquery');
var Isotope = require('../../../assets/vendors/isotope.pkgd.js');

const styles = {
  root: {
    display: 'flex',
    flexWrap: 'wrap',
    justifyContent: 'space-around',
  },
  gridList: {
    width: 500,
    height: 500,
    overflowY: 'auto',
    marginBottom: 24,
  },
};

const EventsList = React.createClass({
    propTypes: {
        events: PropTypes.array
    },
    componentDidMount: function() {
        setTimeout(() => {
            var iso = new Isotope('.events-grid', {
                itemSelector: '.event-card',
                layoutMode: 'masonry'
            });
        }, 100);
    },
    componentDidUpdate: function() {
      var iso = new Isotope( '.events-grid', {
        itemSelector: '.event-card',
        layoutMode: 'masonry'
      });

    },
    render: function() {
        let cards = this.props.events && this.props.events.map((event) => {
          return (<EventCard key={JSON.stringify(event.geoMarker)} event={event} />)
        });

        return (
          <div className='events-grid'>
              {cards}
          </div>
        )
    }
});

export default EventsList;
