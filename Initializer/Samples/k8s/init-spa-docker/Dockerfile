FROM registry.cn-hangzhou.aliyuncs.com/{{mirror}}/nginx:1.14.1 AS base
WORKDIR /app
EXPOSE 80

FROM registry.cn-hangzhou.aliyuncs.com/{{mirror}}/node:21-slim AS build
ENV NODE_OPTIONS=--max_old_space_size=8192
WORKDIR /app

COPY package.json ./

RUN npm config set registry https://registry.npmmirror.com
# RUN npm config set unsafe-perm true
# RUN npm install
RUN yarn 
# RUN npm config set unsafe-perm false

COPY . .

ARG buildEnv
RUN npm run build:${buildEnv}

FROM base AS final
WORKDIR /app
COPY --from=build /app/dist /usr/share/nginx/html
COPY default.conf /etc/nginx/conf.d/default.conf

# COPY docker-entrypoint.sh /
# RUN chmod +x /docker-entrypoint.sh
# ENTRYPOINT ["/docker-entrypoint.sh"]

CMD ["nginx", "-g", "daemon off;"]
