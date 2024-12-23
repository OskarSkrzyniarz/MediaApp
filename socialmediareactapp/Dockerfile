# Use the official Node.js image to build the React app
FROM node:latest AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Use the official Nginx image to serve the React app
FROM nginx:1.26.2-alpine

# Create the /usr/share/nginx/html directory if it doesn't exist
RUN mkdir -p /usr/share/nginx/html

# Copy build files from the build stage to the Nginx html directory
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80