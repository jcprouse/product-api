This uses in-memory version of entity framework. Dummy data is written in on app load. The contents can be found in ExampleData.cs.

An ID property has been added to Models. This will auto increment when inserted into db. This is required to perform an absolute ordering on returned records.

I've added an offset/filter to the get-all product API - it'll default to first 10 record but can be overwritten in the querystring.

In a production solution, these steps would also need to be taken:
- Determine the mandtatory fields and add validation. For example, a product must always have a name.
- Add auditing to all write events.
- Add authentication checks to the API.

Optional additional work may include:
- Add global error handling middleware. This prevents the needs for the try/catch in the controller and the respective tests.
- Add custom ordering against the filter
- Add a maximum to the filter's limit. This prevents a user from returning everything in one big hit.