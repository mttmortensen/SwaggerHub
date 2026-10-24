# Swagger Hub Dashboard

A Blazor-based dashboard that aggregates multiple API endpoints in a clean, responsive interface using Bootstrap cards and grid layout.

**Live:** [https://api.mortensens.cc/hub](https://api.mortensens.cc/hub/)

---

## 📌 Overview

This project provides a centralized place to view and access multiple Swagger API documentation endpoints, styled as a dark "control room" console with live stats. It is built with **Blazor WebAssembly** and plain CSS.

### ✅ Features
- **Live health checks**: every 60s (and on demand) the browser fetches each API's OpenAPI spec; the fetch time is the response time, and a failed fetch marks the API as down.
- **Spec stats**: version, endpoint and operation counts (by HTTP method) are read straight from each spec.
- **Recent checks**: a strip of the last 30 checks and a response-time trend per API, kept for the current browser session.
- **Responsive**: the services table scrolls inside its panel on small screens.

### Adding an API
Add an entry to `Models/ApiCatalog.cs` with its name, base path, Swagger UI link and **raw OpenAPI JSON** URL. The spec is fetched from the browser, so it must be served from `api.mortensens.cc` (same origin as the hub) or send CORS headers.

> Running locally (`dotnet run`), every API shows as **Down**: the specs are cross-origin from `localhost` and don't send CORS headers. In production the hub and the APIs share an origin.

---

## 🛠 Tech Stack
- **Blazor WebAssembly** (.NET 9)
- **C# (Razor Components)**
- **Inter / JetBrains Mono** (Google Fonts)

---

## 📂 Project Structure
```
├───Components   StatCard, ServiceRow, Sparkline, Footer
├───Layout       MainLayout, NavBar
├───Models       ApiLink, ApiCatalog, ApiStatus
├───Pages        SwaggerHomeView (the dashboard)
├───Services     ApiHealthService (spec fetch + parsing)
└───wwwroot
    ├───css
    └───images
```

## 🌍 Deployment
- **Hosted on:** [https://api.mortensens.cc/hub](https://api.mortensens.cc/hub/)
- **Reverse Proxy:** NGINX with SSL (via Cloudflare Tunnel)

---

## 🔮 Future Enhancements
- Add search and filtering for APIs.
- Server-side history for 30-day uptime and request traffic.
- Integrate authentication for private APIs.

---

### 👤 Author
**Matt Mortensen**  

---
## 📜 License
This project is released under the **MIT License**.  
You are free to use, modify, and distribute this project with attribution.

[MIT License](https://opensource.org/licenses/MIT)
