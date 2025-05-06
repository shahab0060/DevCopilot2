module.exports = {
    plugins: [
        require('postcss-extract-media-query')({
            output: {
                path: 'site/packs/medias',
                name: '[name]-[query].[ext]'
            }
        })
    ]
};
