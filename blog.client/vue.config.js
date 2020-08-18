const path = require("path");

module.exports = {
  "transpileDependencies": [
    "vuetify"
  ],
  outputDir: path.resolve(__dirname, "../blog.Web/wwwroot"),
  // outputDir: "G:\\Projects\\Health Programs\\EkonomicActivityPermits\\EconomicActivityPermits\\wwwroot",
  assetsDir: "static",
  chainWebpack: config => {
    if(config.plugins.has('extract-css')) {
      const extractCSSPlugin = config.plugin('extract-css')
      extractCSSPlugin && extractCSSPlugin.tap(() => [{
        filename: 'static/css/[name].css',
        chunkFilename: 'static/css/[name].css'
      }])
    }
  },
  configureWebpack: {
    output: {
      filename: 'static/js/[name].js',
      chunkFilename: 'static/js/[name].js'
    }
  }
}