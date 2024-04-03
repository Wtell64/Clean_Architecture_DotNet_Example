import { fileURLToPath, URL } from 'node:url'
import { defineConfig, splitVendorChunkPlugin } from 'vite'
import vue from '@vitejs/plugin-vue'


// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        vue(), splitVendorChunkPlugin()
    ],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    build: {
        outDir: '../../publish/vue',
    },
    base: '/app/',
    rollupOptions: {
        // https://rollupjs.org/configuration-options/
        output: {
            manualChunks: {
                pdfmake: ['pdfmake']
            }
        }
    },
})
