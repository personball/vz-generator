FROM {{baseImage}} AS base
WORKDIR /app
EXPOSE 80

COPY ./website /usr/share/nginx/html
COPY default.conf /etc/nginx/conf.d/default.conf.template

CMD ["nginx", "-g", "daemon off;"]
