FROM node:latest as node
RUN mkdir -p app
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod
FROM nginx:latest
COPY --from=node /app/dist/poke-fall /usr/share/nginx/html