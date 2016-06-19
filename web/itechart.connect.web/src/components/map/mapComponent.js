import React, { PropTypes } from 'react'
var moment = require('moment');

import EventsContainer from '../../containers/eventsContainer.js';

const MinskCenter = {
    Latitude: 53.900719,
    Longitude: 27.558903
};

const MapComponent = React.createClass({
    propTypes: {
      events: PropTypes.array.isRequired,
      showEventsList: PropTypes.bool,
      draggableMarker: PropTypes.bool
    },
    componentDidMount: function() {
      L.mapbox.accessToken = 'pk.eyJ1Ijoic3NtaXJub3YiLCJhIjoiY2lwazg5bXU5MDA3dnZibnE4ams0cjFtbCJ9.qmmwcN7jdnEcJjXG3Wvk0w';

      let geojson = this.props.events.map((e) => {
          return {
              "type": "Feature",
              "geometry": {
                  "type": "Point",
                  "coordinates": [e.geoMarker.lat, e.geoMarker.lng]
              },
              "properties": {
                  "image": e.img,
                  "name": e.name,
                  "description": e.description,
                  "from": moment.unix(e.dateFrom).format('LLLL'),
                  "to": moment.unix(e.dateTo).format('LLLL'),
                  "marker-color": "#ff8888",
                  "marker-size": "large",
                  "marker-symbol": "bar"
              }
          }
      });

      var map = L.mapbox.map('map', 'mapbox.streets')
          .setView([MinskCenter.Latitude, MinskCenter.Longitude], 13);

      var myLayer = L.mapbox.featureLayer().setGeoJSON(geojson);

      var draggableMarker;
      if (this.props.draggableMarker) {
          draggableMarker = L.marker(new L.LatLng(MinskCenter.Latitude, MinskCenter.Longitude), {
              icon: L.mapbox.marker.icon({
                  'marker-color': 'ff8888'
              }),
              draggable: true
          });
          draggableMarker.bindPopup('Just added event');
          draggableMarker.addTo(map);
      }

      myLayer.on('layeradd', function(e) {
          var marker = e.layer,
              feature = marker.feature;

          var content = '<h2>' + feature.properties.name + '<\/h2>' +
            '<img src="' + feature.properties.image + '" />' +
            '<p>From: ' + feature.properties.from + '<br \/>' +
            'to: ' + feature.properties.to + '<\/p>';

          // http://leafletjs.com/reference.html#popup
          marker.bindPopup(content, {
              closeButton: false,
              minWidth: 320
          });
      });

      // Add features to the map
      myLayer.setGeoJSON(geojson).addTo(map);

    },
    render: function(){
      return (
        <div className='map-component-container'>
          <div id='map'></div>
          {this.props.showEventsList ?  (<div className='map-events-list'><div><EventsContainer /></div></div>): null}
        </div>
      );
    }
});

MapComponent.propTypes = {
}

export default MapComponent
