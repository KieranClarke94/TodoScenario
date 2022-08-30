describe("Todos", () => {
  it("should load the todo page", () => {
    OpenTodos();

    cy.contains("h2", "Todo Items").should("exist");
    cy.contains("h3", "Pending").should("exist");
    cy.contains("h3", "Complete").should("exist");
    cy.contains("li", "Test Todo 1").should("exist");
    cy.contains("li", "Test Todo 2").should("exist");
    cy.contains("li", "Test Todo 3").should("exist");
    cy.contains("button", "Add new todo").should("exist");
  });

  it("should allow you to create a todo", () => {
    OpenTodos();
    cy.contains("button", "Add new todo").should("exist").click();

    cy.get("#name").should("exist");
    cy.get("#name").focus().type("Test Todo");
    cy.get("#completed").should("exist");
    cy.get("#completed").click();

    cy.intercept("POST", "/api/todos", (req) => {
      req.reply({
        status: 200,
        body: req.body,
      });
    }).as("create");

    cy.contains("button", "Confirm").should("exist");
    cy.contains("button", "Confirm").click();

    cy.wait("@create");

    cy.get("@create")
      .its("request.body")
      .then((body) => {
        expect(body).to.have.property("state", "Complete");
        expect(body).to.have.property("description", "Test Todo");
      });
  });

  it("should allow you to edit a todo", () => {
    OpenTodos();
    cy.contains("li", "Test Todo 1").should("exist");
    cy.contains("li", "Test Todo 1").click();

    cy.get("#name").should("have.value", "Test Todo 1");
    cy.get("#completed").should("not.be.checked");

    cy.get("#name").focus().clear().type("Test Todo");
    cy.get("#completed").click();

    cy.intercept(
      "PUT",
      "/api/todos/3e31a10b-9e6b-4f58-bbc3-fa476934b3a1",
      (req) => {
        req.reply({
          status: 200,
          body: req.body,
        });
      }
    ).as("update");

    cy.contains("button", "Confirm").should("exist");
    cy.contains("button", "Confirm").click();

    cy.wait("@update");

    cy.get("@update")
      .its("request.body")
      .then((body) => {
        expect(body).to.have.property("state", "Complete");
        expect(body).to.have.property("description", "Test Todo");
      });
  });

  function OpenTodos(): void {
    cy.intercept(
      { method: "GET", pathname: "/api/todos" },
      { fixture: "todos" }
    ).as("todos");

    cy.visit("/");

    cy.wait("@todos");
  }
});
