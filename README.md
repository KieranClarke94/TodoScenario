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

What technical and functional assumptions did you make when implementing your solution?
* Functional
 * Decided that a modal would be the best fit for the UI actions. I assumed having the ability to edit/create Todos without having to leave the page was a must.
* Technical
 * Decided on using an in memory store for the list of Todos due to the time pressure. Would have added a NoSQL database, such as MongoDB if time allowed due to the speed of queries over using an SQL DB.
 * Included an nginx implemention to bypass CORs issues while using APIs in a container from a separate client.
 * Decided that following the incremental approach to the scenarios would not have been efficient between Scenario 1 and 2. Would have caused issues with the ability to test the Todo list.

Briefly explain your technical design and why do you think is the best approach to this problem.
* My solution was heavily guided by the time constaints and by what I am most experienced with. It would more than likely have been quicker to implement using node for the API, but as I'm more experienced with .NET Core and Docker. I decided that would be the best use of my time.
* Backend
 * Decided on implementing a .NET Core Container for ease of use when running up the application on another machine. Tried to keep everything simple with the only requirement being you have to run the single command to get up and running.
 * However, using the docker approach did cause problems with CORS and being able to access the APIs from the client. Hence the implementation of the nginx server in order to proxy to the container.
* FrontEnd
 * Decided on using react-sweet-state in order to create stores in which to keep track of Todos in the Client. The use of this package, allows for application wide use of a global instance of the list of Todos. Which works well when working with React components.
Other than that, the solution itself is pretty simple.

If you were unable to complete any user stories, outline why and how would you have liked to implement them.
* I was able to complete all the scenarios, though it did take me longer than what was suggested.
* If I had more time, I would have liked to improve in a few areas:
 - MongoDB integration in the .NET to store the Todos so the data would persist
 - Formik integration in the UI for the input for better tracking of validation with the use of Yup.
 - I also apologise for the hideous styling, I would have liked to work on that a bit longer.
