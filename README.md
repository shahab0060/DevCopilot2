🧠 DevCopilot –  Clean Architecture Solution Generator
DevCopilot is an open-source web-based tool that helps developers generate production-ready .NET Core solutions in seconds. With support for Clean Architecture, multilingual apps, and SQL Server integration, DevCopilot reads your database schema and generates complete backend + optional frontend layers—fully structured and ready to use.

⚡ Used in 100+ real-world projects over the past year!

🌐 Live Demo
You can try DevCopilot right now at:

👉 https://dev.runweb.ir

🚀 Key Features
✅ .NET Core 9 + SQL Server Support

🏗️ Clean Architecture Generation (Domain, Application, Infrastructure, API)

⚙️ Reads from Your Database (Tables, Columns, Relationships)

🧑‍💻 Optional Frontend & Views

🌍 Multilingual App Support (i18n-ready)

💡 Error Handling, DTOs, Mappings – all included

🖱️ One-Click Solution Generation

🛠️ Fully Customizable & Open-Source

💻 How to Use
🟢 Option 1: Use the Published Web App (Recommended)
Go to https://dev.runweb.ir

Create or load your project and entity definitions (or connect to your DB).

Click Generate Solution.

Click Generate Project Files.

Download your fully working project ZIP file.

🧑‍💻 Option 2: Run Locally
You need .NET Core 9 SDK and SQL Server installed.

Clone this repository:

bash
Copy
Edit
git clone https://github.com/your-username/DevCopilot.git
Open the solution in Visual Studio or VS Code.

Run the project:

bash
Copy
Edit
dotnet run --project src/DevCopilot.Web
Follow the same steps as the web app, but this time your generated files will be saved directly to your PC in your chosen directory.

📁 Project Output Structure
cpp
Copy
Edit
YourSolution/
├── YourProject.API/
├── YourProject.Application/
├── YourProject.Domain/
├── YourProject.Infrastructure/
├── YourProject.Web/ (optional)
Includes:

Repositories, Services, Controllers

DTOs and AutoMapper

Error Handling Middleware

Localization Support

Razor or API frontends (configurable)

✨ Screenshots
<img width="2234" height="1443" alt="image" src="https://github.com/user-attachments/assets/14448fdb-a328-45c1-ab93-e6a7f98c208e" />


📦 Tech Stack
✅ .NET Core 9

🗄️ SQL Server

🌐 ASP.NET Core Web App (Blazor/Minimal API/Razor)

🎨 Optional: Frontend Views

📚 AutoMapper, FluentValidation, Serilog (etc.)

🧩 Extensibility & Customization
Fully open-source and modular.

Customize generation templates to match your team's coding standards.

Add support for other databases or frontend frameworks.

📫 Contact & Community
Made with ❤️ by Shahab Bakhtiari
📧 Email: sh.bakhtiari0060@gmail.com
🔗 LinkedIn: @shahab-bakhtiari

✅ License
This project is licensed under the MIT License – see the LICENSE file for details.

🙌 Support the Project
If you find this project helpful, please ⭐ star the repo and share it with your network. Contributions and pull requests are welcome!
