FROM node:18-alpine

WORKDIR /nightphotofrontend

COPY ./package.json   .

RUN npm install --silent

COPY . .

EXPOSE 3000

# Start your frontend application
CMD [ "npm", "run", "dev" ]