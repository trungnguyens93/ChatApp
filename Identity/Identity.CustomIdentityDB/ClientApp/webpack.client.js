// Declare variable for using
const path = require("path");
const { merge } = require("webpack-merge");
const baseConfig = require("./webpack.base.js");

// Declare input and output path
const APP_DIR = path.resolve(__dirname, "src");
const BUILD_DIR = path.resolve(__dirname, "dist");
const PUBLIC_DIR = path.resolve(__dirname, "public");

// Configure input and output path when building resource
const config = {
  // Setting webpack-dev-server ở thư mục PUBLIC_DIR với cổng 9000
  devServer: {
    contentBase: PUBLIC_DIR,
    port: 9000,
    open: true,
    historyApiFallback: true,
  },
  entry: APP_DIR + "/index.js",
  output: {
    path: BUILD_DIR,
    filename: "bundle.js",
  },
  mode: "development",
};

module.exports = merge(baseConfig, config);
