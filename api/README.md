# CodeHunt - API
DotNetCore 3.1 Web API

## Clean code refactor
- [x] Move DB into infrastructure
- [x] introduce repositories and services (rename existing service to repos and pass through new services)
- [x] Push Domain logic into services from repos
- [x] Push Domain logic into services from controllers
- [x] Split controllers to have single responsibilities
- [x] Make use of Async throughgout and add Async to signatures
- [x] Add exception handling and error codes
- [x] Split API into Areas
- [x] Use "Codes" for the user to collectableItems things, otherwise make ExternalIds GUIDs
- [ ] Use Filters for validation
- [ ] Use Request Objects
- [ ] Use schema definitions

## Backlog
- [ ] Require login to access anything in the admin area
- [ ] Introduce game status' (Offline, Ready, Started, Finished)
- [ ] Wiki / Documentation
- [ ] Get Active games from cache
- [ ] Tests (Unit and Acceptance)
- [ ] Containerize 
- [ ] Pipelines
- [ ] Consider using generic repository and specification pattern

## Design
### Architecture
https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures

https://www.oreilly.com/content/how-a-restful-api-server-reacts-to-requests/

### Handling Errors
https://stackify.com/web-api-error-handling/
https://docs.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-3.1

### Patterns
https://deviq.com/repository-pattern/
https://deviq.com/specification-pattern/

Database: C:\Users\%username%