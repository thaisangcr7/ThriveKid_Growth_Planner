# ThriveKid – A Parent Growth Planner

ThriveKid is a full-stack web app designed for parents to **track and support their child's development** through smart, meaningful tools. Built from scratch using best practices in backend and frontend development.

## 👶 Project Purpose

As a first-time parent, I quickly realized how overwhelming — and rewarding — this journey can be. There’s so much happening day to day: feedings, sleep cycles, growth milestones, new behaviors, learning goals... and trying to remember it all while also showing up for your family.

I built **ThriveKid** to help with precisely that.

This isn’t just another project — it’s a real tool I’m designing to support parents like me who want to stay present and involved without feeling lost in the chaos.

Whether it's tracking how your baby is eating, sleeping, or reaching new milestones — or planning what to teach next — ThriveKid helps make parenting feel a little more manageable, and a lot more intentional.

It’s built with care.

## 🛠 Tech Stack

| Layer        | Technology / Tools                                                                 |
|--------------|--------------------------------------------------------------------------------------|
| **Frontend** | [React 18](https://reactjs.org/) – component-based UI with Virtual DOM              |
|              | React Router DOM – for client-side routing                                          |
|              | Tailwind CSS *(planned)* – utility-first styling for fast, mobile-friendly UI       |
|              | Axios – HTTP client for API requests                                                |
|              | ESLint + Prettier – code linting and formatting                                     |
| **Backend**  | [ASP.NET Core 8 Web API](https://learn.microsoft.com/en-us/aspnet/core/introduction)|
|              | RESTful API structure using Controllers + Services + DTOs                           |
|              | Dependency Injection (built-in .NET DI container)                                   |
|              | FluentValidation *(planned)* – for clean input validation logic                     |
|              | ILogger – built-in .NET logging support                                              |
| **Database** | [SQLite](https://www.sqlite.org/index.html) – lightweight dev database              |
|              | [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) ORM              |
|              | Migrations – for schema version control                                             |
|              | SeedData.cs – auto-load initial content for testing                                 |
| **DevOps**   | Git & GitHub – version control and collaboration                                    |
|              | GitHub Actions *(planned)* – for CI/CD automation                                   |
|              | Azure App Service *(planned)* – for production deployment                           |
|              | xUnit + EF Core InMemory *(planned)* – for backend unit testing                     |
| **Dev Tools**| Visual Studio 2022 (Windows) – primary backend development IDE                      |
|              | Visual Studio Code (Mac) – frontend development + dual-system workflow              |
|              | Swagger & Postman – for API testing and documentation                               |
|              | GitHub Desktop / CLI – for commits and repository management                        |

---

## 🚀 Features (in progress)

- ✅ **Child Profiles** – track child info, age, gender, etc.
- ✅ **Feeding Logs** – log breastmilk, formula, solids, etc.
- ✅ **Milestones** – record key development achievements
- 🔜 **Sleep Logs** – nap/sleep tracking
- 🔜 **Learning Goals** – age-appropriate progress
- 🔜 **Toy & Activity Suggestions** – curated by age
- 🔜 **Reminders Engine** – smart nudges for parents
- 🔜 **PDF Export** – share with family, caregivers, or doctors

---

## 📁 Folder Structure

```bash
ThriveKid/
├── backend/                  # ASP.NET Core 8 Web API
│   └── ThriveKid.API/
│       ├── Controllers/
│       ├── DTOs/
│       ├── Models/
│       ├── Services/
│       ├── Validators/
│       ├── Data/
│       ├── Middleware/
│       ├── Migrations/
│       ├── Program.cs
│       ├── appsettings.json
│       └── ThriveKid.API.csproj
│
├── frontend/                 # React PWA frontend
│   └── thrivekid-app/
│       ├── public/
│       ├── src/
│       │   ├── components/
│       │   ├── pages/
│       │   ├── services/
│       │   ├── hooks/
│       │   └── context/
│       ├── App.tsx
│       ├── main.tsx
│       ├── index.css
│       ├── manifest.json
│       └── service-worker.ts
│
├── .gitignore
├── thrivekid.sln
└── README.md
```
---

## 🧠 Engineering Practices

- Clean architecture: Controllers, Services, DTOs, Validators
- Database seeding for demo/test data
- EF Core + SQLite setup with migrations
- Scalable folder structure for real-world growth
- Goal: Azure cloud deployment + unit testing with xUnit

---

## 📎 Status

Actively building | Last major commit: Milestones + FeedingLogs full CRUD  
More endpoints, validation, and frontend UI coming soon!

---

## 🔗 Author

**Sang Thai** – Automation Analyst, Aspiring Developer  
GitHub: [thaisangcr7](https://github.com/thaisangcr7)

---
