// cryptoInterop.js
// Must be loaded before blazor.webassembly.js and must attach functions to window.

window.encryptRequest = async function(publicKeyPem, base64Data, contentType) {
    try {
        // ---------- helpers ----------
        function base64ToUint8Array(b64) {
            const binary = atob(b64);
            const len = binary.length;
            const arr = new Uint8Array(len);
            for (let i = 0; i < len; i++) arr[i] = binary.charCodeAt(i);
            return arr;
        }
        function uint8ArrayToBase64(u8) {
            let binary = '';
            const chunk = 0x8000;
            for (let i = 0; i < u8.length; i += chunk) {
                binary += String.fromCharCode.apply(null, u8.subarray(i, i + chunk));
            }
            return btoa(binary);
        }

        // ---------- import RSA public key (SPKI PEM) ----------
        const pem = publicKeyPem
            .replace("-----BEGIN PUBLIC KEY-----", "")
            .replace("-----END PUBLIC KEY-----", "")
            .replace(/\s+/g, "");
        const der = base64ToUint8Array(pem);
        const publicKey = await crypto.subtle.importKey(
            "spki",
            der,
            { name: "RSA-OAEP", hash: "SHA-256" },
            false,
            ["encrypt"]
        );

        // ---------- prepare data ----------
        // base64Data is a base64 string of the full request body bytes
        const dataBytes = base64ToUint8Array(base64Data);

        // ---------- AES-GCM key + IV ----------
        const aesKey = await crypto.subtle.generateKey(
            { name: "AES-GCM", length: 256 },
            true,
            ["encrypt", "decrypt"]
        );
        const rawAes = new Uint8Array(await crypto.subtle.exportKey("raw", aesKey));
        const iv = crypto.getRandomValues(new Uint8Array(12)); // 96-bit IV

        // ---------- AES-GCM encrypt ----------
        const encrypted = await crypto.subtle.encrypt(
            { name: "AES-GCM", iv },
            aesKey,
            dataBytes
        );
        const encryptedBytes = new Uint8Array(encrypted); // ciphertext + tag (browser places tag at end)

        // ---------- RSA-OAEP encrypt AES key ----------
        const encryptedKeyBuf = await crypto.subtle.encrypt(
            { name: "RSA-OAEP" },
            publicKey,
            rawAes
        );
        const encryptedKeyBytes = new Uint8Array(encryptedKeyBuf);

        // ---------- return base64 strings ----------
        return {
            cipher: uint8ArrayToBase64(encryptedBytes),
            encryptedKey: uint8ArrayToBase64(encryptedKeyBytes),
            iv: uint8ArrayToBase64(iv),
            contentType: contentType
        };
    } catch (err) {
        console.error("encryptRequest error:", err);
        throw err;
    }
};