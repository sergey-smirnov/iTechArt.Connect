'use strict';

var path = require('path');
var webpack = require('webpack');

var HtmlWebpackPlugin = require('html-webpack-plugin');
var ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports = {
    devtool: 'source-maps',
    entry: [
        'webpack-dev-server/client?http://localhost:3000',
        'webpack/hot/only-dev-server',
        'react-hot-loader/patch',
        './src/index.js'
    ],
    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: 'js/app.bundle.js'
    },
    resolveLoader: {
        root: [
            path.resolve(__dirname, 'node_modules')
        ]
    },
    plugins: [
        new webpack.HotModuleReplacementPlugin(),
        new HtmlWebpackPlugin({
            filename: 'index.html',
            title: 'One More Redux Application',
            hash: false,
            template: './templates/index.ejs'
        }),
        new ExtractTextPlugin('css/app.css')
    ],
    module: {
        loaders: [{
            test: /\.js$/,
            exclude: /node_modules/,
            include: path.join(__dirname, 'src'),
            loader: 'react-hot-loader/webpack',
        }, {
            test: /\.js$/,
            loader: 'babel-loader',
            exclude: /node_modules/,
            include: path.join(__dirname, 'src'),
            query: {
                presets: ['react', 'es2015']
            }
        }, {
            test: /\.css/,
            loader: 'style!css'
        }, {
            test: /\.less$/,
            exclude: /node_modules/,
            include: path.join(__dirname, 'assets', 'styles'),
            loader: ExtractTextPlugin.extract(
                // activate source maps via loader query
                'css?sourceMap!' +
                'autoprefixer?browsers=last 3 version!' +
                'less?sourceMap'
            )
        }]
    }
};
