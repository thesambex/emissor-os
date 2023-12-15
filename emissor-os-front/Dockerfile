FROM node:alpine3.19 as build

WORKDIR /app

RUN npm cache clean --force

COPY . .

RUN npm install
RUN npm run build --prod

FROM nginx:alpine3.18

COPY --from=build /app/dist/emissor-os-front/browser /usr/share/nginx/html
COPY /nginx.conf  /etc/nginx/conf.d/default.conf

EXPOSE 80