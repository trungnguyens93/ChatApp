module.exports = {
  // Ánh xạ code đã biên dịch trở về đoạn code gốc
  devtool: "source-map",
  module: {
    rules: [
      {
        // Ung dung Bable cho tat ca cac file *.js
        test: /\.(js|jsx)$/,
        loader: "babel-loader",
        // Yeu cau Babel khong bao gom cac phan trong node-modules
        exclude: /node-modules/,
        options: {
          presets: [
            "@babel/preset-env",
            ["@babel/preset-react", { runtime: "automatic" }],
          ],
        },
      },
      {
        test: /\.css$/i,
        use: ["style-loader", "css-loader"],
      },
      {
        test: /\.svg$/,
        loader: "file-loader",
      },
    ],
  },
};
