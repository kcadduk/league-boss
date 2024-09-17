# Use Microsoft's .NET image as the base
FROM mcr.microsoft.com/devcontainers/dotnet:8.0

# Install Node.js (you can adjust the version)
RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs

# Optionally install Yarn (package manager)
RUN npm install -g yarn
