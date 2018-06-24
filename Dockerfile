FROM microsoft/dotnet:2.0-sdk

# app working path
WORKDIR /app/dist

# copy csproj and restore as distinct layers
COPY ./LeanEngine ./

# restore dependencies
RUN dotnet restore LeanEngine.sln

# set port
EXPOSE 3000

# set aspnet url
ENV ASPNETCORE_URLS http://0.0.0.0:3000

# copy and build everything else
COPY . ./

# build and publish to working path 
RUN dotnet publish -o /app/dist -c Release 

# if you want to show the files in working path
# RUN cd /app/dist && ls

# if you want to build it on LeanEngine, MUST input the assembly name in lean-cli.
ENTRYPOINT ["dotnet", "/app/dist/aspnet-mvc-core-getting-started.dll"]