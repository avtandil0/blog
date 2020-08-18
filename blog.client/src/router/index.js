import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Articles from '../components/Articles.vue'
import Questions from '../components/Questions.vue'
import Article from '../components/Article.vue'
import QuestionDetails from '../components/QuestionDetails.vue'
import QuillEditor from '../components/QuillEditor.vue'
import Videos from '../components/Videos.vue'
import AddVideo from '../components/AddVideo.vue'
import Login from '../views/Login.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    redirect: '/home',
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/home',
    component: Home,
    props: (route) => ({ ...route.params }),
    children: [
      {
        path: '/',
        redirect: '/Category1',
        props: (route) => ({ ...route.params }),
      },
      {
        path: '/Main',
        name: 'Main',
        component: Articles,
        props: (route) => ({ ...route.params }),
      },
      {
        path: '/Main/:id',
        name: 'MainDetails',
        component: Article,
        props: (route) => ({ ...route.params }),
      },
      {
        path: '/Questions',
        name: 'Questions',
        component: Questions,
        props: (route) => ({ ...route.params }),
      },
      {
        path: '/Videos',
        name: 'Videos',
        component: Videos,
        props: (route) => ({ ...route.params }),
      },
      {
        path: '/AddVideo',
        name: 'AddVideo',
        component: AddVideo,
        props: (route) => ({ ...route.params }),
      },
      {
        path: '/QuillEditor',
        name: 'QuillEditor',
        component: QuillEditor,
        props: (route) => ({ ...route.params }),
      },
      {
        path: '/QuillEditor/:id',
        name: 'QuillEditorDetails',
        component: QuillEditor,
        props: (route) => ({ ...route.params }),
      },
      {
        path: '/Question/:id',
        name: 'QuestionDetails',
        component: QuestionDetails,
        props: (route) => ({ ...route.params }),
      },
      {
        path: '/Category1',
        name: 'Category1',
        component: Articles,
        props: (route) => ({ ...route.params, categoryId:'1' }),
      },
      {
        path: '/Category1/:id',
        name: 'Category1Details',
        component: Article,
        props: (route) => ({ ...route.params }),
      },
      {
        path: '/Category2',
        name: 'Category2',
        component: Articles,
        props: (route) => ({ ...route.params, categoryId: '2' }),
      },
      {
        path: '/Category2/:id',
        name: 'Category2Details',
        component: Article,
        props: (route) => ({ ...route.params }),
      },
      {
        path: '/Category3',
        name: 'Category3',
        component: Articles,
        props: (route) => ({ ...route.params }),
      },
      {
        path: '/Category3/:id',
        name: 'Category3Details',
        component: Article,
        props: (route) => ({ ...route.params }),
      },

    ]
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  }
]

const router = new VueRouter({
  routes
})

export default router
