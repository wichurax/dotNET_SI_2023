FROM node:latest
WORKDIR /app
COPY . .
RUN npm install
RUN npm install -g @angular/cli

EXPOSE 4200

CMD ["npm", "start"]
