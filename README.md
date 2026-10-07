# AWS Cloud Practice (.NET / C#)

A collection of .NET 8 / C# projects demonstrating AWS cloud integration patterns, including serverless functions with **AWS Lambda**, object storage with **Amazon S3**, email notifications with **Amazon SES**, and **ASP.NET Core Web API**.

---

## 📂 Project Structure

```text
awscloudprac/
├── MyConsoleApp/
│   ├── MyConsoleApp.csproj              # .NET 8 Console Application
│   └── Program.cs                       # Console entrypoint / playground
│
└── MyWebAPIApp/
    ├── MyWebAPIApp.csproj               # ASP.NET Core Web API (.NET 8)
    ├── Program.cs                       # Web API entrypoint with Swagger & endpoints
    ├── ApplicationController.cs         # Resume upload controller orchestrating S3 & SES
    ├── AwsS3StorageService.cs           # S3 integration: uploads files & generates pre-signed URLs
    ├── AwsSesEmailService.cs            # SES integration: sends email notifications
    ├── appsettings.json                 # Application configuration
    ├── appsettings.Development.json     # Development settings
    │
    └── SingleCSharpLambda/
        ├── SingleCSharpLambda.csproj    # AWS Lambda function project
        ├── Function.cs                  # Lambda handler implementing string transformation & logging
        ├── aws-lambda-tools-defaults.json # Deployment configuration for AWS CLI tools
        └── Readme.md                    # Visual Studio & CLI deployment guide for Lambda
```

---

## 🛠️ Components and Architecture

### 1. `MyWebAPIApp` (ASP.NET Core Web API)
- **Target Framework**: .NET 8.0 (`net8.0`)
- **Key Packages**:
  - `AWSSDK.S3` (v4.0.103.4)
  - `AWSSDK.SimpleEmail` (v3.*)
  - `Amazon.Lambda.AspNetCoreServer` (v10.2.2)
  - `Swashbuckle.AspNetCore` (OpenAPI / Swagger)
- **Features**:
  - **Resume Upload Workflow (`ApplicationController.cs`)**:
    - Accepts file uploads via HTTP `POST /Application`.
    - Streams the uploaded file directly to S3 via `AwsS3StorageService`.
    - Generates a pre-signed URL with a 10-minute validity window.
    - Sends an email alert to the recruitment team via `AwsSesEmailService`.
  - **AWS S3 Integration (`AwsS3StorageService.cs`)**:
    - Uses `AmazonS3Client` (region: `eu-west-2`).
    - Stores objects with unique GUID keys.
    - Generates secure pre-signed download URLs.
  - **AWS SES Integration (`AwsSesEmailService.cs`)**:
    - Uses `AmazonSimpleEmailServiceClient` (region: `eu-west-1`).
    - Constructs and transmits MIME/plain-text email messages.

### 2. `SingleCSharpLambda` (AWS Lambda Function)
- **Target Framework**: .NET (`net10.0` / `dotnet8` runtime)
- **Handler**: `SingleCSharpLambda::SingleCSharpLambda.Function::FunctionHandler`
- **Features**:
  - Configured with `SystemTextJson.DefaultLambdaJsonSerializer` for JSON serialization.
  - Logs execution details via `LambdaLogger`.
  - Pre-configured deployment settings in `aws-lambda-tools-defaults.json`.

### 3. `MyConsoleApp` (C# Console Application)
- **Target Framework**: .NET 8.0 (`net8.0`)
- Standalone executable for local testing and scripting.

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- AWS CLI configured with appropriate IAM credentials (`aws configure`)
- (Optional) [Amazon.Lambda.Tools](https://github.com/aws/aws-extensions-for-dotnet-cli) global CLI tool

### Building the Solution

From the root directory or individual project folders:

```bash
# Build the Web API
dotnet build MyWebAPIApp/MyWebAPIApp.csproj

# Build the Console App
dotnet build MyConsoleApp/MyConsoleApp.csproj

# Build the Lambda Function
dotnet build MyWebAPIApp/SingleCSharpLambda/SingleCSharpLambda.csproj
```

### Running the Web API Locally

```bash
cd MyWebAPIApp
dotnet run
```

Access Swagger UI in your browser at:
`https://localhost:<port>/swagger`

---

## ☁️ Deploying the Lambda Function

Using the `dotnet-lambda` CLI tool:

1. Install or update the tool:
   ```bash
   dotnet tool install -g Amazon.Lambda.Tools
   ```

2. Deploy the Lambda function:
   ```bash
   cd MyWebAPIApp/SingleCSharpLambda
   dotnet lambda deploy-function
   ```

---

## 🔒 Configuration and Security

- Do not commit AWS credentials (`AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`) or production configuration files.
- Use IAM Roles or AWS CLI profiles (`~/.aws/credentials`) for authentication in development and deployment.
- Verify S3 bucket permissions and SES verified email addresses/identities before running in production.

---

## 📄 License

This project is created for cloud learning and experimentation.
