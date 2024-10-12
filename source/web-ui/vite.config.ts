import viteReact from '@vitejs/plugin-react'
import {TanStackRouterVite} from '@tanstack/router-vite-plugin'
import {defineConfig, loadEnv} from 'vite';


// https://vitejs.dev/config/
/** @type {import('vite').UserConfig} */
export default defineConfig(({mode}) => {
    const env = loadEnv(mode, process.cwd(), '');

    return {
        plugins: [viteReact(), TanStackRouterVite()],
        server: {
            port: parseInt(env.VITE_PORT),
            proxy: {
                '/api': {
                    target: process.env.services__api__https__0 ||
                        process.env.services__api__http__0,
                    changeOrigin: true,
                    rewrite: (path) => path.replace(/^\/api/, ''),
                    secure: false,
                }
            }
        },
        build: {
            outDir: 'dist',
            rollupOptions: {
                input: './index.html'
            }
        }
    }
})
