# TodoScenario


How long did you spend on your solution?
- 6/7 hours

How do you build and run your solution?
* .NET
  * Requires Docker
  * Navigate to the .NET directory
  * On a CLI run "docker-compose up -d --build"
  * This will start the Todo Container and an nginx server used to reverse proxy between the container and the client.
* JS
  * Requires NPM
  * Navigate to the JS directory
  * On a CLI run "npm i && npm start"
  * Once this is done, navigate to localhost:3000 to use the application.
