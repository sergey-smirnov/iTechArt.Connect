var webpack = require('webpack');
var WebpackDevServer = require('webpack-dev-server');
var config = require('./webpack.config');

new WebpackDevServer(webpack(config), {
    publicPath: config.output.publicPath,
    hot: true,
    historyApiFallback: true,
    contentBase: './dist'
}).listen(3000, 'localhost', function(err, result) {
    if (err) {
        return console.log(err);
    }

    console.log('Listening on port 3000. Open up http://localhost:3000/ in your browser.');
});
