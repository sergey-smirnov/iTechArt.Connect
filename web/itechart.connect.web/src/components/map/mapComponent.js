import React, { PropTypes } from 'react'

const MinskCenter = {
    Latitude: 53.900719,
    Longitude: 27.558903
};

const MapComponent = React.createClass({
    propTypes: {
      markers: PropTypes.array
    },
    componentDidMount: function() {
        mapboxgl.accessToken = 'pk.eyJ1Ijoic3NtaXJub3YiLCJhIjoiY2lwazg5bXU5MDA3dnZibnE4ams0cjFtbCJ9.qmmwcN7jdnEcJjXG3Wvk0w';
        var map = new mapboxgl.Map({
            container: 'map',
            center: new mapboxgl.LngLat(MinskCenter.Longitude, MinskCenter.Latitude),
            zoom: 12,
            style: 'mapbox://styles/mapbox/streets-v9'
        });
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
