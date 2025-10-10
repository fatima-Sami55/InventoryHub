# Reflection: Building InventoryHub with Microsoft Copilot

Throughout this project, I used Microsoft Copilot as a coding assistant to develop and optimize the InventoryHub full-stack application.

### Integration
Copilot helped me generate initial integration code that connected the Blazor front-end to the Minimal API back-end. It suggested correct HTTP client syntax and improved readability.

### Debugging
When integration issues appeared (wrong route, CORS, and malformed JSON), Copilot quickly pinpointed them and suggested precise fixes such as `app.UseCors(...)` and JSON deserialization error handling.

### JSON Structuring
Copilot helped format the API response with nested JSON objects for category data. It also validated the output structure using Postman.

### Optimization
Finally, Copilot suggested caching strategies and code refactoring to reduce redundant calls and improve app performance.

### Reflection
This project taught me how to use Copilot effectively as a coding collaborator — not to rely on it blindly, but to use its suggestions critically. It sped up development, improved accuracy, and made debugging smoother.
