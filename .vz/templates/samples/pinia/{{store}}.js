import { defineStore } from 'pinia'
import { get{{model}} } from '@/api/{{store}}(camelCase)'

export const use{{store}} = defineStore('{{store}}(camelCase)', {
    state: () => ({
      {{model}}(camelCase): null
    }),
    actions: {
      clear{{model}}() {
        this.{{model}}(camelCase) = null
      },
      // 获取用户信息
      async get{{model}}() {
        const res = await get{{model}}()
        if (res) {
          this.{{model}}(camelCase) = res
        } 
        // else {
        //   this.{{model}}(camelCase) = { name: '优盾数字' }
        // }
      },
    },
  })
  