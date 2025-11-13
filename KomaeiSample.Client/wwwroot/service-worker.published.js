self.importScripts('./service-worker-assets.js');

const CACHE_NAME = 'KomaeiSample-cache-v' + (self.assetsManifest && self.assetsManifest.version ? self.assetsManifest.version : Date.now());
const APP_SHELL_FILES = [
    './',
    'index.html',
    // the assets list will be added programmatically below
];

const assets = (self.assetsManifest && self.assetsManifest.assets) ? self.assetsManifest.assets.map(a => a.url) : [];
const assetsToCache = APP_SHELL_FILES.concat(assets);

// Install: cache app shell + assets
self.addEventListener('install', event => {
    self.skipWaiting();
    event.waitUntil(
        caches.open(CACHE_NAME)
            .then(cache => cache.addAll(assetsToCache))
    );
});

// Activate: delete old caches, then claim clients. If old caches were removed -> notify clients
self.addEventListener('activate', event => {
    event.waitUntil(
        caches.keys().then(keys => {
            const deletePromises = keys.map(key => {
                if (key !== CACHE_NAME) return caches.delete(key);
                return Promise.resolve(false);
            });
            return Promise.all(deletePromises).then(results => {
                // If any cache was deleted (there was an older cache), notify clients
                const hadOldCache = results.some(r => r === true);
                return hadOldCache;
            });
        }).then(hadOldCache => {
            return self.clients.claim().then(() => hadOldCache);
        }).then(hadOldCache => {
            if (hadOldCache) {
                // Tell pages a new version is available
                self.clients.matchAll({ includeUncontrolled: true }).then(clients => {
                    clients.forEach(client => client.postMessage({ type: 'NEW_VERSION_AVAILABLE' }));
                });
            }
        })
    );
});

// Fetch: cache-first for same-origin assets, network fallback; update cache in background when network available.
self.addEventListener('fetch', event => {
    if (event.request.method !== 'GET') return;

    const requestUrl = new URL(event.request.url);

    // Handle navigation requests (app shell)
    if (event.request.mode === 'navigate' && requestUrl.origin === location.origin) {
        event.respondWith(
            caches.match('index.html').then(resp => resp || fetch(event.request))
        );
        return;
    }

    // For same-origin resources, try cache first, then network and update cache
    if (requestUrl.origin === location.origin) {
        event.respondWith(
            caches.match(event.request).then(cached => {
                const fetchPromise = fetch(event.request).then(networkResponse => {
                    if (networkResponse && networkResponse.ok) {
                        caches.open(CACHE_NAME).then(cache => cache.put(event.request, networkResponse.clone()));
                    }
                    return networkResponse;
                }).catch(() => null);
                return cached || fetchPromise;
            })
        );
        return;
    }

    // For cross-origin (CDN) just do network
    event.respondWith(fetch(event.request));
});