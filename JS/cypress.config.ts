import { defineConfig } from "cypress";

export default defineConfig({
  requestTimeout: 10000,
  retries: {
    runMode: 2,
    openMode: 0,
  },
  e2e: {
    setupNodeEvents(on, config) {},
    baseUrl: "http://localhost:3000",
    supportFile: false,
  },
});
