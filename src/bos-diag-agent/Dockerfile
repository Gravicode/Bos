FROM mcr.microsoft.com/dotnet/core/runtime:8.0
WORKDIR /app

# We need `ps`
RUN apt-get update \
  && apt-get install -y procps \
  && rm -rf /var/lib/apt/lists/*

COPY . .
ENTRYPOINT ["dotnet", "bos-diag-agent.dll"]