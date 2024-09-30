# LingoLabs - Language Learning Platform with Handwriting Recognition
LingoLabs is an innovative online language learning platform that integrates a handwriting recognition system. The platform allows users to practice writing characters in different languages and have their handwriting recognized and evaluated in real time. The system is built on a robust microservices architecture, with ASP.NET Core handling the core logic and Python managing handwriting recognition.

# Video Demo: [LingoLabs - Demo](https://www.youtube.com/watch?v=6O5fYmA819k&ab_channel=ClaudiuChichirau)

# Features
1. **Microservices Architecture**:
The application is based on a microservices architecture, making it highly scalable and maintainable. 
3. **Handwriting Recognition**:
A Python-based microservice processes handwritten characters, using a multilayer neural network to recognize and evaluate user input.
4. **ASP.NET Core Backend**:
The primary backend logic is handled by an ASP.NET Core 8.0 server that manages requests and data communication between the front-end and various microservices.
5. **Web Interface (Blazor & Bootstrap)**:
A modern web interface built with Blazor and Bootstrap provides an intuitive and user-friendly experience for learners.
6. **Data Management with EntityFrameworkCore**:
Data is stored and managed using EntityFrameworkCore connected to a MySQL database, ensuring efficient access and storage.
7. **HTTP Requests Handling**:
HTTP method requests are managed through MediatR, while FluentValidation ensures that all data passing through the system is properly validated for consistency and integrity.

# Technology Stack
- **Backend**: C#, ASP.NET Core 8.0, MediatR, FluentValidation, EntityFrameworkCore
- **Frontend**: Blazor, Bootstrap
- **Database**: MySQL
- **Handwriting Recognition Microservice**: Python & Flask, OpenCV, NumPy, Multilayer Neural Network
