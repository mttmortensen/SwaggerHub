# Swagger Hub Dashboard

A Blazor-based dashboard that aggregates multiple API endpoints in a clean, responsive interface using Bootstrap cards and grid layout.

**Live:** [https://api.mortensens.xyz/hub](https://api.mortensens.xyz/hub)

---

## 📌 Overview

This project provides a centralized place to view and access multiple Swagger API documentation endpoints. It is built with **Blazor** for client-side rendering and styled using **Bootstrap** for responsive design.

### ✅ Features
- **Dynamic API Listing**: Displays grouped API links in an elegant card-based layout.
- **Responsive Design**: Built with Bootstrap for mobile-friendly UI.
- **Reusable Components**: Uses Razor components for modularity (`ApiCard.razor`, `ApiGroup.razor`).
- **Simple Models**:
  - `ApiLink` – Holds API name and URL.
  - `ApiGroup` – Holds a group name and its related API links.

---

## 🛠 Tech Stack
- **Blazor WebAssembly**
- **Bootstrap 5**
- **C# (Razor Components)**

---

## 📂 Project Structure
```
├───Components
├───Layout
├───Models
├───obj
├───Pages
├───Properties
└───wwwroot
    ├───css
    └───images          
```

### Models:
```csharp
// ApiLink.cs
public record ApiLink(string Name, string URL);

// ApiGroup.cs
public class ApiGroup
{
    public string? GroupName { get; set; }
    public List<ApiLink>? Links { get; set; } = new();
}
```

## 🌍 Deployment
- **Hosted on:** [https://api.mortensens.xyz/hub](https://api.mortensens.xyz/hub)
- **Reverse Proxy:** NGINX with SSL (via Cloudflare Tunnel)

---

## 🔮 Future Enhancements
- Add search and filtering for APIs.
- Add dark/light theme toggle.
- Integrate authentication for private APIs.

---

### 👤 Author
**Matt Mortensen**  

---
## 📜 License
This project is released under the **MIT License**.  
You are free to use, modify, and distribute this project with attribution.

[MIT License](https://opensource.org/licenses/MIT)
