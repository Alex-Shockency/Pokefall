FROM node:lts-slim As builder

RUN mkdir -p app
WORKDIR /app
COPY package.json /app
RUN npm install
COPY . /app
RUN npm run build --prod

FROM nginx

COPY --from=builder /app/dist/poke-fall /usr/share/nginx/html