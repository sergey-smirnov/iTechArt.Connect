import React, { PropTypes } from 'react'
var moment = require('moment');

const MinskCenter = {
    Latitude: 53.900719,
    Longitude: 27.558903
};

const MapComponent = React.createClass({
    propTypes: {
      events: PropTypes.array.isRequired
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
                  "marker-color": "#3ca0d3",
                  "marker-size": "large",
                  "marker-symbol": "bar"
              }
          }
      });

      var map = L.mapbox.map('map', 'mapbox.light')
          .setView([MinskCenter.Latitude, MinskCenter.Longitude], 12);

      var myLayer = L.mapbox.featureLayer().setGeoJSON(geojson);

      myLayer.on('layeradd', function(e) {
          var marker = e.layer,
              feature = marker.feature;

          // Create custom popup content
          // var popupContent = '<a target="_blank" class="popup" href="' + feature.properties.name + '">' +
          //     '<img src="' + feature.properties.image + '" />' +
          //     feature.properties.name +
          //     '</a>';

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
        </div>
      );
    }
});

MapComponent.propTypes = {
}

export default MapComponent
